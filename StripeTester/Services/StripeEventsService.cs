using Stripe;
using StripeTester.Services.Interfaces;

namespace StripeTester.Services
{
    public class StripeEventsService:IStripeEventsService
    {
        public StripeEventsService()
        {

        }

        public bool AddData(string json)
        {
            var stripeEvent = EventUtility.ParseEvent(json);

            var fileName = "";
            
        var result = stripeEvent.Type switch
        {
            Events.PaymentIntentCreated => fileName = stripeEvent.Type.ToString() + ".json",
            Events.PaymentMethodAttached => fileName = stripeEvent.Type.ToString() + ".json",
            Events.PriceCreated => fileName = stripeEvent.Type.ToString() + ".json",
            Events.CheckoutSessionCompleted => fileName = stripeEvent.Type.ToString() + ".json",
            Events.ProductCreated => fileName = stripeEvent.Type.ToString() + ".json",
            Events.ChargeSucceeded => fileName = stripeEvent.Type.ToString() + ".json",
            Events.PaymentIntentSucceeded => fileName = stripeEvent.Type.ToString() + ".json",
            _ => "No case available"
        };  

            // Handle the event
           
                var paymentIntent = stripeEvent.Data.Object;
                Newtonsoft.Json.JsonConvert.SerializeObject(paymentIntent);
                System.IO.File.WriteAllText(@".\StripeData\" + fileName, json);
                return true;
            
            
        }                
    }
}
