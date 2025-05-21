using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Identity;
using SEProject.DataAccess.Model;
using SEProject.Pages;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SEProject.Tests
{
    [TestClass]
    public class RegisterPageModelTest
    {
        [TestMethod]
        public async Task OnPostAsync_ValidRegistration_RedirectsToIndex()
        {
            // Arrange
            var mockUserManager = new Mock<UserManager<AppUser>>(
                Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);

            var mockSignInManager = new Mock<SignInManager<AppUser>>(
                mockUserManager.Object,
                Mock.Of<Microsoft.AspNetCore.Http.IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<AppUser>>(),
                null, null, null, null);

            mockUserManager.Setup(m => m.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                           .ReturnsAsync(IdentityResult.Success);

            mockSignInManager.Setup(m => m.SignInAsync(It.IsAny<AppUser>(), false, null))
                             .Returns(Task.CompletedTask);

            var pageModel = new RegisterModel(mockUserManager.Object, mockSignInManager.Object)
            {
                Input = new RegisterModel.InputModel
                {
                    Username = "testuser",
                    Password = "Password123!"
                }
            };

            // Act
            var result = await pageModel.OnPostAsync();

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToPageResult));
            var redirect = result as RedirectToPageResult;
            Assert.AreEqual("/Index", redirect.PageName);
        }
    }
}
