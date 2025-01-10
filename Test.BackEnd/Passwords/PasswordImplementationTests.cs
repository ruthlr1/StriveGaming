using back_end.Services;

namespace Test.BackEnd.Passwords
{
    public class PasswordImplementationTests
    {
        [Fact]
        public void When_PasswordIsEightCharacters_Then_IsValid()
        {
            // arrange
            PasswordService passwordService = new PasswordService();

            // act
            var errMsg = passwordService.ValidatePassword("password");

            // assert
            Assert.NotEqual("Password must be between 7-14 characters in length", errMsg);
        }

        [Fact]
        public void When_PasswordIsSixCharacters_Then_IsNotValid()
        {
            // arrange
            PasswordService passwordService = new PasswordService();

            // act
            var errMsg = passwordService.ValidatePassword("passwo");

            // assert
            Assert.Equal("Password must be between 7-14 characters in length", errMsg);
        }

        [Fact]
        public void When_PasswordIsFifteenCharacters_Then_IsNotValid()
        {
            // arrange
            PasswordService passwordService = new PasswordService();

            // act
            var errMsg = passwordService.ValidatePassword("MyPasswordQueen");

            // assert
            Assert.Equal("Password must be between 7-14 characters in length", errMsg);
        }
    }
}
