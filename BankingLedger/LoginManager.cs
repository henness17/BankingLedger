using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingLedger
{
  /// <summary>
  /// Contains logic used for user login management.
  /// </summary>
  public static class LoginManager
  {
    #region Properties
    /// <summary>
    /// Gets the <see cref="User"/> that is currently logged in.
    /// </summary>
    public static User CurrentUser { get; private set; }

    /// <summary>
    /// Gets whether a user is logged in.
    /// </summary>
    public static bool UserIsLoggedIn => LoginManager.CurrentUser.Id != 0;

    private static List<User> Users { get; set; }

    private static User NoUser { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes static members of the <see cref="LoginManager"/> class.
    /// </summary>
    static LoginManager()
    {
      LoginManager.NoUser = new User(0, "No", "User", "DNidSA3G=&}o_Y", "Vyq^1iihGYpSkBP");
      LoginManager.CurrentUser = LoginManager.NoUser;
      LoginManager.Users = new List<User>();
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Creates a new account.
    /// </summary>
    /// <param name="firstName">The user's first name.</param>
    /// <param name="lastName">The user's last name.</param>
    /// <param name="username">The user's username.</param>
    /// <param name="password">The user's password.</param>
    public static void CreateNewAccount(string firstName, string lastName, string username, string password)
    {
      if (string.IsNullOrEmpty(firstName))
        throw new ArgumentNullException(nameof(firstName));
      if (string.IsNullOrEmpty(lastName))
        throw new ArgumentNullException(nameof(lastName));
      if (string.IsNullOrEmpty(username))
        throw new ArgumentNullException(nameof(username));
      if (string.IsNullOrEmpty(password))
        throw new ArgumentNullException(nameof(password));

      int id = LoginManager.Users.Count + 1;
      User newUser = new User(id, firstName, lastName, username, password);
      LoginManager.Users.Add(newUser);
    }

    /// <summary>
    /// Attempts to log the user in using username and password.
    /// </summary>
    /// <param name="username">The user's username.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>Whether or not the login attempt was successful.</returns>
    public static bool LogIn(string username, string password)
    {
      User currentUser = LoginManager.Users.FirstOrDefault(user => user.Username == username && user.Password == password);
      if (currentUser != null)
      {
        LoginManager.CurrentUser = currentUser;
        return true;
      }
      else
        return false;
    }

    /// <summary>
    /// Logs the current user out.
    /// </summary>
    public static void LogOut()
    {
      LoginManager.CurrentUser = LoginManager.NoUser;
    }

    /// <summary>
    /// Determines whether a user with the given username exists.
    /// </summary>
    /// <param name="username">The username to check for existence.</param>
    /// <returns>Whether a user with that username exists.</returns>
    public static bool UserExists(string username)
    {
      if (LoginManager.Users.Any(user => user.Username == username))
        return true;
      return false;
    }
    #endregion
  }
}
