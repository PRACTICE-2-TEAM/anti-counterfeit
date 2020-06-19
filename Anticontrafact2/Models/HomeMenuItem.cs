using System;
using System.Collections.Generic;
using System.Text;

namespace Anticontrafact2.Models
{
    public enum MenuItemType
    {
        CheckGood,
        CheckShop,
        ReportOnGood,
        ReportOnShop,
        ReportsStatus

    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
