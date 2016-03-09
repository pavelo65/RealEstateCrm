﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApp.Entities;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class HousingEditModel
    {
        public int EditId { get; private set; }
        
        [Display(Name = "Фамилия")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string MidleName { get; set; }
        
        [Display(Name = "Отчество")]
        public string LastName { get; set; }

        [Display(Name = "Описание")]
        public string Comment { get; set; }

        [Display(Name = "Цена")]
        public double Cost { get; set; }
        
        public AddressSelectionModel Address { get; set; }

        [Required]
        [Display(Name = "Телефон 1 для связи")]
        public string Phone1 { get; set; }

        [Display(Name = "Телефон 2 для связи")]
        public string Phone2 { get; set; }

        [Display(Name = "Телефон 3 для связи")]
        public string Phone3 { get; set; }

        [Display(Name =  "Тип жилья")]
        public DropDownViewModel HouseType { get; set; }

        [Display(Name = "Дата освобождения объекта")]
        public DateTime EndDate { get; set; }

        public string FullAddress { get; set; }
        public HousingEditModel()
        {
        }

        public static HousingEditModel Create(ApplicationDbContext context, Housing housing)
        {
            var allCities = context.Cities.Include(x => x.Districts).ToList();
            var allStreets = context.Streets.ToList();
            var typesHousings = context.TypesHousing.ToList();

            var item = new HousingEditModel
            {
                EditId = housing.Id,
                Comment = housing.Comment,
                FirstName = housing.FirstName,
                LastName = housing.LastName,
                MidleName = housing.MidleName,
                Cost = housing.Sum,
                Address = new AddressSelectionModel(allCities, allStreets, housing)
                {
                    HouseNumber = housing.House,
                    HouseBuilding = housing.Building,
                    Room = housing.Room
                },
                HouseType = new DropDownViewModel
                {
                    Id = housing.TypesHousingId,
                    Items = typesHousings.Select(x => new SelectListItem {Value = x.Id.ToString(), Text = x.Name })
                }
            };

            if (housing.Phones != null && housing.Phones.Any())
            {
                item.Phone1 = housing.Phones.FirstOrDefault(x => x.Order == 0)?.Number;
                item.Phone2 = housing.Phones.FirstOrDefault(x => x.Order == 1)?.Number;
                item.Phone3 = housing.Phones.FirstOrDefault(x => x.Order == 2)?.Number;
            }

            var addressParts = new List<string>();
            if (housing.City != null)
            {
                addressParts.Add(housing.City.Name);
            }

            if (housing.District != null)
            {
                addressParts.Add(housing.District.Name);
            }

            if (housing.Street != null)
            {
                addressParts.Add(housing.Street.Name);
            }

            addressParts.Add(housing.House);
            addressParts.Add(housing.Building);
            addressParts.Add(housing.Room);
            
            item.FullAddress = addressParts.Where(x => !string.IsNullOrEmpty(x)).Aggregate("", (x, y) => x + ", " + y);

            return item;
        }


        public void UpdateEntity(Housing item)
        {
            item.FirstName = FirstName;
            item.MidleName = MidleName;
            item.LastName = LastName;
            item.Sum = Cost;
            item.Comment = Comment;
            item.CityId = Address.City.Id;
            item.DistrictId = Address.District.Id;
            item.StreetId = Address.Street.Id;
            item.House = Address.HouseNumber;
            item.Building = Address.HouseBuilding;
            item.Room = Address.Room;
            item.TypesHousingId = HouseType.Id;

            UpdatePhone(item, 0, Phone1);
            UpdatePhone(item, 1, Phone2);
            UpdatePhone(item, 2, Phone3);
        }

        private static void UpdatePhone(Housing item, int order, string phone)
        {
            var housingPhone = item.Phones.SingleOrDefault(x => x.Order == order);
            if (housingPhone != null)
            {
                housingPhone.Number = phone;
            }
            else if(!string.IsNullOrEmpty(phone))
            {
                housingPhone = new HousingPhone { Number = phone, Order = order };
                item.Phones.Add(housingPhone);
            }
        }
    }
}
