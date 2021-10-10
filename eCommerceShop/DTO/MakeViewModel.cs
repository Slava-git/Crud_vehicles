using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerceShop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerceShop.DTO
{
    public class MakeViewModel
    {
        public Model Model { get; set; }
        public IEnumerable<Make> Makes { get; set; }
        public IEnumerable<SelectListItem> selectListItem(IEnumerable<Make> makes)
        {
            List<SelectListItem> MakeList = new List<SelectListItem>();

            SelectListItem listItem = new SelectListItem { };
            foreach (Make make in makes)
            {
                listItem = new SelectListItem {
                    Text = make.Name,
                    Value = make.Id.ToString()
                };
                MakeList.Add(listItem);
            }
            return MakeList;
        }
    }
}
