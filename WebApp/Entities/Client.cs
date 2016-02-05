﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public enum Gender
    {
        Male,
        Female
    }

    public class Client
    {

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string  MidleName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Gender? Gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Resource { get; set; }

        public int DistrictId { get; set; }

        public virtual District District { get; set; }

        public int Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MinSum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MaxSum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateMeeting { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ContractSum { get; set; }
    }
}