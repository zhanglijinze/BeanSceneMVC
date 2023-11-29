
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeanScene.Tests.Controllers
{
    public class MenuItemsControllerTests: BeanSceneTestBase
    {
        //Unit test method naming convention:class_method_other-info_expected-result
        [Fact]
        public async void MenuItemsController_Index_ReturnsView() 
        {

            //Arrange (get everything ready)

            var controller = new MenuItemsController(_context, _webHostEnvironment);



            //Act (run the code being tested)

            var result = await controller.Index();
            //Assert (check/assert the result)
            //Make sure a View is returned
             Assert.IsType<ViewResult>(result);

        
        }

        //Unit test method naming convention:class_method_other-info_expected-result
        [Fact]
        public async void MenuItemsController_Details_ReturnsView()
        {

            //Arrange (get everything ready)

            var controller = new MenuItemsController(_context, _webHostEnvironment);

            const int EXISTING_MENUITEM_ID = 1;

            //Act (run the code being tested)

            var result = await controller.Details(id:EXISTING_MENUITEM_ID);
            //Assert (check/assert the result)
            //Make sure a View is returned
            Assert.IsType<ViewResult>(result);


        }

        //Unit test method naming convention:class_method_other-info_expected-result
        [Fact]
        public async void MenuItemsController_Details_NoMenuItemId_ReturnsNotFound()
        {

            //Arrange (get everything ready)

            var controller = new MenuItemsController(_context, _webHostEnvironment);


            //Act (run the code being tested)

            var result = await controller.Details(id:null!);
            //Assert (check/assert the result)
        
            Assert.IsType<NotFoundResult>(result);


        }


        [Fact]
        public async void MenuItemsController_Details_NonExistingMenuItem_ReturnsNotFound()
        {

            //Arrange (get everything ready)

            var controller = new MenuItemsController(_context, _webHostEnvironment);

            const int EXISTING_MENUITEM_ID = 12;

            //Act (run the code being tested)

            var result = await controller.Details(id: EXISTING_MENUITEM_ID);
            //Assert (check/assert the result)
       
            Assert.IsType<NotFoundResult>(result);


        }

        [Fact]
        public async void MenuItemsController_Create_Success_ReturnsRedirect()
        {

            //Arrange (get everything ready) - create controller and add objectvalidator to stop null exception in TryValidateModel()

            var controller = new MenuItemsController(_context, _webHostEnvironment);
            controller.ObjectValidator = new ObjectValidator();
            //Valid menu item
            MenuItem menuItem = new MenuItem
            {
                Id = 52,

                Name = "Testing Mains",

                Description = "Main-1 test",

                Price = 52,

                Photo = "Main-1.jpg",

                IsAllergenFree = true,

                IsDiaryFree = true,

                IsGlutenFree = true,

                IsVegan = true,

                IsVegetarian = true,

                MenuCategoryId = 2,
            };

            //Act (run the code being tested)

            var result = await controller.Create(menuItem);


            //Assert (check/assert the result)
            //Should be redirected to the index page (same controller)
            // (controller name = null, controller action = "Index")
            var redirectionToActionResult =  Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectionToActionResult.ControllerName);
            Assert.Equal("Index", redirectionToActionResult.ActionName);


        }


        [Fact]
        public async void MenuItemsController_Create_InvalidMenuCategory_ReturnsNotFound()
        {

            //Arrange (get everything ready) - create controller and add objectvalidator to stop null exception in TryValidateModel()

            var controller = new MenuItemsController(_context, _webHostEnvironment);
            controller.ObjectValidator = new ObjectValidator();

            //InValid menu item (bad category)
            MenuItem menuItem = new MenuItem
            {
                Id = 89,

                Name = "Testing Mains",

                Description = "Main-1 test",

                Price = 52,

                Photo = "Main-1.jpg",

                IsAllergenFree = true,

                IsDiaryFree = true,

                IsGlutenFree = true,

                IsVegan = true,

                IsVegetarian = true,

                MenuCategoryId = 55,// bad menu category
            };

            //Act (run the code being tested)

            var result = await controller.Create(menuItem);


            //Assert (check/assert the result) - not found
            
             Assert.IsType<NotFoundObjectResult>(result);
           


        }

        [Fact]
        public async void MenuItemsController_Create_InvalidMenuItem_ReturnsView()
        {

            //Arrange (get everything ready) - create controller and add objectvalidator to stop null exception in TryValidateModel()

            var controller = new MenuItemsController(_context, _webHostEnvironment );
            controller.ObjectValidator = new ObjectValidator();

            //InValid menu item (bad menu item)
            MenuItem menuItem = new MenuItem
            {
                Id = 89,

                Name = "Testing Mainsffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff" +
                "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff" +
                "fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff" +
                "ffffffffffffffffffffffffffffffffffffffffffffff",

                Description = "Main-1 test",

                Price = 52,

                Photo = "Main-1.jpg",

                IsAllergenFree = true,

                IsDiaryFree = true,

                IsGlutenFree = true,

                IsVegan = true,

                IsVegetarian = true,

                MenuCategoryId = 1,
            };

            //Act (run the code being tested)

            var result = await controller.Create(menuItem);


            //Assert - reload of create view

            var viewResult = Assert.IsType<ViewResult>(result);

           Assert.Null(viewResult.ViewName);



        }



    }
}
