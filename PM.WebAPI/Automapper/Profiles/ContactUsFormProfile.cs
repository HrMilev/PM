﻿using AutoMapper;
using PM.Common.Models.Rest;
using PM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PM.WebAPI.Automapper.Profiles
{
    public class ContactUsFormProfile : Profile
    {
        public ContactUsFormProfile()
        {
            CreateMap<ContactUsFormRestModel, ContactUsForm>();
            CreateMap<ContactUsForm, ContactUsFormRestModel>();
        }
    }
}
