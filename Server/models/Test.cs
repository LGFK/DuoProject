using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary;

namespace Server.models;

public record class RequestAsyncResult
{
    private RequestAsyncResult(bool success, byte[] data, string errorMessage)
    {
        Success = success;
        Data = data;
        ErrorMessage = errorMessage;
    }

    public bool Success { get; init; }
    public byte[] Data { get; init; }
    public string ErrorMessage { get; init; }

    public static RequestAsyncResult ConnectionException(byte[] data, string message) =>
        new RequestAsyncResult(false, data, message);

    public static RequestAsyncResult RequestException(byte[] data, string message) =>
        new RequestAsyncResult(false, data, $"Failed to establish connection to the server. {message}");

    public static RequestAsyncResult DataRequestAsyncResult(byte[] data, string message) =>
        new RequestAsyncResult(false, data, $"An error occurred while sending/receiving the request. {message}");
}


/*internal class Test
{

}*/




/*public class OrderProcessor
{
    private const int ProcessableNumberOflineItem = 15;
    public ProcessOrderResult Process(Order? order)
    {
        if (!IsOrdesProcessable(order))
        {
            return ProcessOrderResult.NotProcessable();
        }
        if (order!.Items.Count > ProcessableNumberOflineItem)
        {
            return ProcessOrderResult.HasTooManyLineItems(order.Id);
        }
        if(order.Status !=  OrderStatus.ReadyToProcess)
        {
            return ProcessOrderResult.NotReadyForProcessing(order.Id);
        }
        return ProcessOrderResult.Successful(ordeId.Id);
    }
}

public static bool IsOrdesProcessable(Order? order)
{
    return order is not null &&
           order.IsVerified &&
           order.Items.Any();
}

public record class ProcessOrderResult
{
    private ProcessOrderResult(long orderId,string message)
    {
        OrderId= orderId;
        Message= message;
    }
    public long OrderId { get; init; }
    public string Message { get; init; }
    public static ProcessOrderResult NotProcessable() =>
        new(default, $"The order is not processable");
    public static ProcessOrderResult NotReadyForProcessing(long orderId) =>
     new(orderId, $"The order{orderId} isn't ready to process");
    public static ProcessOrderResult HasTooManyLineItems (long orderId) =>
     new(orderId, $"The order{orderId} has too many items");
    public static ProcessOrderResult Successful(long ordeId) =>
        new(ordeId, $"The order {ordeId} was successfully processed");
}*/

/*public record class RequestAsyncResult()
{
    public RequestAsyncResult(byte[] data,string message)
    {
        Data = data;
        ErrorMessage= message;
    }
    public byte[] Data { get; init; }
    public string ErrorMessage { get; init; }
    public static RequestAsyncResult ConnectionExcaption(byte[] data, string message) =>
        new(data, message);
    public static RequestAsyncResult RequestException(byte[] data, string message) =>
    new(data, $"Failed to establish connection to the server.{message}");
    public static RequestAsyncResult DataRequestAsyncResult(byte[] data, string message) =>
    new(data, $"An error occurred while sending/receiving the request.{message}");
}*/