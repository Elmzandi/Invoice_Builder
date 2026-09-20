using FluentAssertions;

namespace InvoiceBuilder.Api.Modules.Users.Authentication.Registration;
public class RegisterRequestValidatorTests
{
    private readonly RegisterRequestValidator _validator = new();

    [Fact]
    public void Validate_ValidRequest_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var request = new RegisterRequest("John", "Doe", "john.doe@example.com", "Password123");
        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeTrue();
    }
    [Fact]
    public void Validate_InvalidRequest_ShouldHaveValidationErrors()
    {
        // Arrange
        var request = new RegisterRequest("", "", "", "");
        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeFalse();
    }
    [Fact]
    public void Validate_InvalidRequest_WhenFirstNameIsEmpty()
    {
        // Arrange
        var request = new RegisterRequest("", "Doe", "john.doe@example.com", "Password123");
        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeFalse();
    }
    [Fact]
    public void Validate_InvalidRequest_WhenLastNameIsEmpty()
    {
        // Arrange
        var request = new RegisterRequest("John", "", "john.doe@example.com", "Password123");
        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeFalse();
    }
    [Fact]
    public void Validate_InvalidRequest_WhenEmailIsEmpty()
    {
        // Arrange
        var request = new RegisterRequest("John", "Doe", "", "Password123");
        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeFalse();
    }
    [Fact]
    public void Validate_InvalidRequest_WhenEmailIsInvalid()
    {
        // Arrange
        var request = new RegisterRequest("John", "Doe", "invalid-email", "Password123");
        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeFalse();
    }
    [Fact]
    public void Validate_InvalidRequest_WhenPasswordIsEmpty()
    {
        // Arrange
        var request = new RegisterRequest("John", "Doe", "john.doe@example.com", "");
        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeFalse();
    }
    [Fact]
    public void Validate_InvalidRequest_WhenPasswordIsTooShort()
    {
        // Arrange
        var request = new RegisterRequest("John", "Doe", "john.doe@example.com", "Pass");
        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeFalse();
    }
    [Fact]
    public void Validate_InvalidRequest_WhenPasswordIsTooLong()
    {
        // Arrange
        var longPassword = new string('a', 129); // 129 characters
        var request = new RegisterRequest("John", "Doe", "john.doe@example.com", longPassword);
        // Act
        var result = _validator.Validate(request);
        // Assert
        result.IsValid.Should().BeFalse();
    }
}