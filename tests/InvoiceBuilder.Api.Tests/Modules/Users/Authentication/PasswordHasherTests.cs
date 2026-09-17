using FluentAssertions;
using InvoiceBuilder.Api.Modules.Users.Authentication;

namespace InvoiceBuilder.Api.Tests.Modules.Users.Authentication;
public class PasswordHasherTests
{
    [Fact]
    public void HashPassword_ShouldReturnHashedPassword()
    {
        // Arrange
        var passwordHasher = new PasswordHasher();
        var password = "TestPassword123!";

        // Act
        var hashedPassword = passwordHasher.HashPassword(password);

        // Assert
        hashedPassword.Should().NotBeNullOrWhiteSpace();
        hashedPassword.Should().NotBe(password);
    }
    [Fact]
    public void VerifyPassword_ShouldReturnTrueForValidPassword()
    {
        // Arrange
        var passwordHasher = new PasswordHasher();
        var password = "TestPassword123!";
        var hashedPassword = passwordHasher.HashPassword(password);

        // Act
        var isValid = passwordHasher.VerifyPassword(password, hashedPassword);

        // Assert
        isValid.Should().BeTrue();
    }
    [Fact]
    public void VerifyPassword_ShouldReturnFalseForInvalidPassword()
    {
        // Arrange
        var passwordHasher = new PasswordHasher();
        var password = "TestPassword123!";
        var hashedPassword = passwordHasher.HashPassword(password);
        var invalidPassword = "WrongPassword123!";

        // Act
        var isValid = passwordHasher.VerifyPassword(invalidPassword, hashedPassword);

        // Assert
        isValid.Should().BeFalse();
    }
    [Fact]
    public void HashPassword_Should_Produce_DifferentHashesForSamePassword()
    {
        // Arrange
        var passwordHasher = new PasswordHasher();
        var password = "TestPassword123!";
        var hashedPassword1 = passwordHasher.HashPassword(password);
        var hashedPassword2 = passwordHasher.HashPassword(password);

        // Assert
        hashedPassword1.Should().NotBe(hashedPassword2);
    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void HashPassword_Should_Throw_Exception_For_Null_Or_Empty_Password(string? password)
    {
        // Arrange
        var passwordHasher = new PasswordHasher();

        // Act
        Action act = () => passwordHasher.HashPassword(password!);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void VerifyPassword_Should_Throw_Exception_For_Null_Or_Empty_Password(string? password)
    {
        // Arrange
        var passwordHasher = new PasswordHasher();
        var hashedPassword = passwordHasher.HashPassword("TestPassword123!");

        // Act
        Action act = () => passwordHasher.VerifyPassword(password!, hashedPassword);

        // Assert
        act.Should().Throw<ArgumentException>();
    }   
}