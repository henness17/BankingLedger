using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingLedger
{
  /// <summary>
  /// Logic that defines the Transaction Manager. The Transaction Manager conducts transactions and
  /// holds records of all transactions made by all users.
  /// </summary>
  public static class TransactionManager
  {
    #region Properties
    private static List<Transaction> Transactions { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes static members of the <see cref="TransactionManager"/> class.
    /// </summary>
    static TransactionManager()
    {
      TransactionManager.Transactions = new List<Transaction>();
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Sends a deposit transaction to the user's account.
    /// </summary>
    /// <param name="amount">The amount to deposit.</param>
    public static void Deposit(double amount)
    {
      Transaction newDeposit = new Transaction(
        TransactionManager.Transactions.Count,
        LoginManager.CurrentUser.Id,
        Transaction.TransactionType.Deposit,
        amount);
      LoginManager.CurrentUser.MakeTransaction(newDeposit);
      TransactionManager.Transactions.Add(newDeposit);
      newDeposit.PostBalance = LoginManager.CurrentUser.DisplayBalance;
    }

    /// <summary>
    /// Sends a withdrawal transaction to the user's account.
    /// </summary>
    /// <param name="amount">The amount to withdraw.</param>
    public static void Withdraw(double amount)
    {
      Transaction newWithdrawal = new Transaction(
        TransactionManager.Transactions.Count,
        LoginManager.CurrentUser.Id,
        Transaction.TransactionType.Withdrawal,
        amount);
      LoginManager.CurrentUser.MakeTransaction(newWithdrawal);
      TransactionManager.Transactions.Add(newWithdrawal);
      newWithdrawal.PostBalance = LoginManager.CurrentUser.DisplayBalance;
    }

    /// <summary>
    /// Prints the user's current account balance.
    /// </summary>
    public static void CheckBalance()
    {
      Console.WriteLine("Your current balance: " + LoginManager.CurrentUser.DisplayBalance);
    }

    /// <summary>
    /// Prints out transaction details for all transactions made by the current user.
    /// </summary>
    public static void ViewTransactions()
    {
      List<Transaction> userTransactions = (TransactionManager.Transactions
        .Where(transaction => transaction.UserId == LoginManager.CurrentUser.Id)).ToList();
      if (userTransactions.Any())
      {
        Console.WriteLine(
          "{0,5} | {1,20} | {2,10} | {3,14} | {4,14} |",
          StringResources.IdCaption,
          StringResources.DateCaption,
          StringResources.TypeCaption,
          StringResources.AmountCaption,
          StringResources.PostBalanceCaption);
        foreach (Transaction transaction in userTransactions)
        {
          Console.WriteLine(
            "{0,5} | {1,20} | {2,10} | {3,14} | {4,14} |",
            transaction.Id,
            transaction.Date.ToString("MM/dd/yyyy h:mm tt"),
            transaction.Type,
            "$" + transaction.Amount.ToString("#,##0.00"),
            transaction.PostBalance);
        }
      }
      else
        Console.WriteLine(StringResources.NoTransactionHistoryMessage);
    }
    #endregion
  }
}
