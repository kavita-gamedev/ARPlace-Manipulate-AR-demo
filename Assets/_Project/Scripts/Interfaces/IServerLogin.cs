public interface IServerLogin {

    
    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    /// <value>The email.</value>
    string Email { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    /// <value>The password.</value>
    string Password { get; set; }

    /// <summary>
    /// Gets a value indicating whether this instance is email valid.
    /// </summary>
    /// <value><c>true</c> if this instance is email valid; otherwise, <c>false</c>.</value>
    bool IsEmailValid { get; }

    /// <summary>
    /// Raises the validate event.
    /// </summary>
    void ValidateCredentials();
}
