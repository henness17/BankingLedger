using System;

namespace BankingLedger
{
  /// <summary>
  /// Contains logic that defines a user. The <see cref="User"/> class contains
  /// information about the account and balance.
  /// </summary>
  public class User
  {
    #region Properties
    /// <summary>
    /// Gets the user's identification number.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets the user's first name.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Gets the user's last name.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Gets the user's username.
    /// </summary>
    public string Username { get; private set; }

    /// <summary>
    /// Gets the user's password.
    /// </summary>
    public string Password { get; private set; }

    /// <summary>
    /// Gets the balance of the user's bank account (in cents).
    /// </summary>
    public int Balance { get; private set; }

    /// <summary>
    /// Gets the string used to display the balance in dollars.
    /// </summary>
    public string DisplayBalance => "$" + this.ConvertCentsToDollars(this.Balance).ToString("#,##0.00");
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="id">The identification number.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <param name="username">The username.</param>
    /// <param name="password">The password.</param>
    public User(int id, string firstName, string lastName, string username, string password)
    {
      if (string.IsNullOrEmpty(firstName))
        throw new ArgumentNullException(nameof(firstName));
      if (string.IsNullOrEmpty(lastName))
        throw new ArgumentNullException(nameof(lastName));
      if (string.IsNullOrEmpty(username))
        throw new ArgumentNullException(nameof(username));
      if (string.IsNullOrEmpty(password))
        throw new ArgumentNullException(nameof(password));

      this.Id = id;
      this.FirstName = firstName;
      this.LastName = lastName;
      this.Username = username;
      this.Password = password;
      this.Balance = 0;
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Performs transaction action on the user's account balance.
    /// </summary>
    /// <param name="transaction">The transaction to perform.</param>
    public void MakeTransaction(Transaction transaction)
    {
      if (transaction == null)
        throw new ArgumentNullException();

      int amountInCents = this.ConvertDollarsToCents(transaction.Amount);
      if (transaction.Type == Transaction.TransactionType.Deposit)
      {
        try
        {
          int test = checked(this.Balance + amountInCents);
          this.Balance += amountInCents;
        }
        catch (OverflowException)
        {
          throw new ArgumentOutOfRangeException(string.Empty, StringResources.DepositExceedsAccountLimitMessage);
        }
      }
      else
      {
        if (this.Balance - amountInCents < 0)
          throw new ArgumentOutOfRangeException(string.Empty, StringResources.WithdrawalExceedsAccountLimitMessage);
        this.Balance -= amountInCents;
      }
    }
    #endregion

    #region Private Methods
    private double ConvertCentsToDollars(int cents)
    {
      double dollars = Math.Floor((double)cents / 100) + (((double)cents % 100) / 100);
      return dollars;
    }

    private int ConvertDollarsToCents(double dollars)
    {
      int cents = (int)(dollars * 100);
      return cents;
    }
    #endregion
  }
}
