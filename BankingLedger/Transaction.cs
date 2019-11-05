using System;

namespace BankingLedger
{
  /// <summary>
  /// Contains properties and enums that define a transaction.
  /// </summary>
  public class Transaction
  {
    #region Constants
    const double ACCOUNT_BALANCE_MAXIMUM = 21474836.47;
    #endregion

    #region Properties
    /// <summary>
    /// Gets the transaction's Id.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets the transaction's occurrence date.
    /// </summary>
    public DateTime Date { get; private set; }

    /// <summary>
    /// Gets the transaction's <see cref="TransactionType"/>.
    /// </summary>
    public TransactionType Type { get; private set; }

    /// <summary>
    /// Gets the <see cref="User"/> Id that made the transaction.
    /// </summary>
    public int UserId { get; private set; }

    /// <summary>
    /// Gets the transaction's amount.
    /// </summary>
    public double Amount { get; private set; }

    /// <summary>
    /// Gets or sets the account's balance after the transaction occurred.
    /// </summary>
    public string PostBalance { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Transaction"/> class.
    /// </summary>
    /// <param name="id">The identification number.</param>
    /// <param name="userId">The owner of the transaction's identification number.</param>
    /// <param name="type">The transaction type.</param>
    /// <param name="amount">The transaction amount.</param>
    public Transaction(int id, int userId, TransactionType type, double amount)
    {
      double roundedAmount = Math.Round(amount, 2, MidpointRounding.ToEven);
      if (roundedAmount <= 0 || roundedAmount > Transaction.ACCOUNT_BALANCE_MAXIMUM)
        throw new ArgumentOutOfRangeException(string.Empty, StringResources.InvalidAmountMessage);
      this.Id = id;
      this.UserId = userId;
      this.Amount = roundedAmount;
      this.Type = type;
      this.Date = DateTime.Now;
    }
    #endregion

    #region Enums
    /// <summary>
    /// Enumeration values for different transaction types.
    /// </summary>
    public enum TransactionType
    {
      /// <summary>
      /// A deposit into an account.
      /// </summary>
      Deposit = 0,

      /// <summary>
      /// A withdrawal from an account.
      /// </summary>
      Withdrawal = 1
    }
    #endregion
  }
}
