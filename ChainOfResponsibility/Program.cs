using System;

namespace ChainOfResponsibility
{
    /*
     Workflow:
     - Sender sendsa message
     - Receiver 1 decides if it is capable fo handling the message or not. if it is not, it will
     pass the message along to the second receiver.
     - Receiver 2 has the same decision to make. if it can handle it will send a response back
     to the sender
     - If response was sent Receiver 3 was not called.

     Traits:
     - Sender is aware of only one receiver
     - each receiver is only aware of the next receiver
     - REceivers process the message on send it down to the chain
     - The sender does not know who received the message
     - The first receiver to handle the message terminates the chain
     - The order of the receivers list matters

    Use chain of reponsibility when:
    - More than one message handler for a message
    - The appropriate handler is not explicitly known by the sender
    - The set of handlers can be dynamically defined

    Benefits:
    - Reduce coupling
    - Dynamicaly manage the message handlers
    - End of chain behavior can be defined appropriately
     */
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var william = new ExpenseHandler(new Employee("Will", 0));
            var mary = new ExpenseHandler(new Employee("Mary", 1000));
            var victor = new ExpenseHandler(new Employee("Victor", 5000));

            william.RegisterNext(mary);
            mary.RegisterNext(victor);

            IExpenseReport report = new ExpenseReport(1500);
            ApprovalResponse response = william.Approve(report);
        }
    }

    internal class ExpenseHandler : IExpenseHander
    {
        private readonly IExpenseApprower _approver;
        private IExpenseHander _next;

        public ExpenseHandler(IExpenseApprower approver)
        {
            _approver = approver;
            _next = EndOfChainExpenseHandler.Instance;
        }

        public void RegisterNext(IExpenseHander next)
        {
            _next = next;
        }

        public ApprovalResponse Approve(IExpenseReport report)
        {
            var respone =_approver.ApproveExpense(report);
            if (respone == ApprovalResponse.BeyondApprovalLimit)
            {
                return _next.Approve(report);
            }

            return respone;
        }
    }

    internal class EndOfChainExpenseHandler : IExpenseHander
    {
        private static readonly EndOfChainExpenseHandler _instance = new EndOfChainExpenseHandler();
        public static EndOfChainExpenseHandler Instance = _instance;

        private EndOfChainExpenseHandler() { }

        public void RegisterNext(IExpenseHander next)
        {
            throw new InvalidOperationException("End of chain handler must be the end of chain");
        }

        public ApprovalResponse Approve(IExpenseReport report)
        {
            return ApprovalResponse.Denied;
        }
    }

    public interface IExpenseHander
    {
        void RegisterNext(IExpenseHander next);
        ApprovalResponse Approve(IExpenseReport report);
    }

    internal class Employee : IExpenseApprower
    {
        private readonly string _name;
        private readonly decimal _approvableAmount;

        public Employee(string name, decimal approvableAmount)
        {
            _name = name;
            _approvableAmount = approvableAmount;
        }

        public ApprovalResponse ApproveExpense(IExpenseReport report)
        {
            return _approvableAmount > report.Amount ? ApprovalResponse.Approved : ApprovalResponse.BeyondApprovalLimit;
        }
    }

    internal interface IExpenseApprower
    {
        ApprovalResponse ApproveExpense(IExpenseReport report);
    }

    public enum ApprovalResponse
    {
        Approved,
        BeyondApprovalLimit,
        Denied
    }

    public class ExpenseReport : IExpenseReport
    {
        public ExpenseReport(decimal amount)
        {
            Amount = amount;
        }

        public decimal Amount { get; }
    }

    public interface IExpenseReport
    {
        decimal Amount { get; }
    }
}
