﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookStore.DesignPattern.Command
{
    //Admin Order Controller
    public interface ICommand
    {
        void Execute(Controller controller);
    }
}
