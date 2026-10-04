
namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            //delete order entity from command object
            var orderId = OrderId.Of(command.OrderId);

            //[orderId] marked it as a stronly type parameter
            var order = await dbContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken);
            if (order is null)
                throw new OrderNotFoundException(command.OrderId);
            dbContext.Orders.Remove(order);
            //save db
            await dbContext.SaveChangesAsync(cancellationToken);
            //return result
            return new DeleteOrderResult(true);
        }
    }
}
