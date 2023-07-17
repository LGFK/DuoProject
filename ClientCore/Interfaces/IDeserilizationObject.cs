using ClientCore.Rusults;
using ComandLibrary;
using CommunicationLibrary;

namespace ClientCore.Interfaces;
public interface IDeserilizationObject
{
    RequestResult<RequestResponseBase> DeserializationObjectFromServer(string requestToReceive);
    RequestResponseBase ChoiceCommand(ComandsLib comandsLib, string? requestToReceive);
}
