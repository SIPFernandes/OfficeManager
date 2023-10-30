using EmailSenderClient.Shared.Areas.Services;
using NotificationsServiceApi.Data.Consts;
using RabbitMQClient.Shared.Areas.Helpers;
using RabbitMQClient.Shared.Data.Models;
using OfficeManager.Shared.Entities;

namespace NotificationsServiceApi.Areas.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailSenderService _emailSender;

        public NotificationService(IEmailSenderService emailSender)
        {
            _emailSender = emailSender;
        }

        public void TreatMessageReceived(object? obj, string message)
        {
            var msg = MessageHelper.GetMessage<MessageModel>(message);

            if (msg != null && msg.PublisherService == 
                ReservationServiceConst.ReservationService &&
                msg.Action == ReservationServiceConst.Actions.Delete.DeleteReservation)
            {                
                if (msg.Content != null)
                {
                    var reservation = MessageHelper.GetMessage<BookingModel>(msg.Content);

                    if (reservation != null)
                    {
                        _emailSender.SendEmailAsync(reservation.UserId,
                            ReservationServiceConst.Actions.Delete.Subject, "Template**");
                    }                    
                }
            }
        }
    }
}
