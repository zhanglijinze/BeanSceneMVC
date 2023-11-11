using BeanSceneMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BeanSceneMVC.ViewModels
{
    public class SittingViewModel
    {
        public Sitting Sitting { get; set; } = null!;

        public SelectList TimeslotList { get; set; } = null!;

        public SelectList SittingTypeList { get; set; } = null!;
        

    }
}
