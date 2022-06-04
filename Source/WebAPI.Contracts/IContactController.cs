﻿using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebAPI.Contracts
{
    public interface IContactController
    {
        IActionResult QueryContacts();
        IActionResult Add(Contact contact);
        IActionResult Merge(Contact contact);
        IActionResult Delete(Contact contact);
    }
}
