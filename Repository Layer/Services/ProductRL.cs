using Microsoft.Extensions.Configuration;
using Repository_Layer.Interface;
using Repository_Layer.Services.Entities;
using System;
using Database_Layer.ProductModel;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using Experimental.System.Messaging;
using System.Net.Mail;
using System.Net;

namespace Repository_Layer.Services
{
    public class ProductRL : IProductRL
    {
        private readonly ProductContext productContext;
        private readonly IConfiguration configuration;

        public ProductRL(ProductContext productContext, IConfiguration configuration)
        {
            this.productContext = productContext;
            this.configuration = configuration;
        }

        public void AddProduct(Database_Layer.ProductModel.ProductPostModel productPostModel,int UserId,string Image)
        {
            try
            {
                var user = productContext.Users.FirstOrDefault(u => u.UserId == UserId);
                


                Product product = new Product();
               
                product.Name = productPostModel.Name;
                product.Price = productPostModel.Price;
                product.Image = Image;
                
                product.UserId = UserId;
                productContext.Products.Add(product);
                productContext.SaveChanges();


                MessageQueue queue;
                //ADD Message To Queue
                if (MessageQueue.Exists(@".\Private$\ProductQueue"))
                {
                    queue = new MessageQueue(@".\Private$\ProductQueue");
                }
                else
                {
                    queue = MessageQueue.Create(@".\Private$\ProductQueue");
                }
                Message MyMessage = new Message();
                MyMessage.Formatter = new BinaryMessageFormatter();
                MyMessage.Body = $"<html><body><p><p style='float:left;margin:0px 10px 10px 0px;'><b>Hi{user.Name}</br>,<br/>Your Order has been successfully placed.</p><p>Product Name {product.Name}<br/>Product ID{product.ProductId}</p><br/>Thankyou for shopping with our Product App<br/><img style='float:left;margin:0px 10px 10px 0px;'src='https://media.istockphoto.com/photos/colorful-soccer-ball-picture-id1315940628?b=1&k=20&m=1315940628&s=170667a&w=0&h=x0fTtUg8TWIVWrpU6AOzQYoU0ZrtRA7OIJE8vCBcEQ0=' width='100' height='100'/><div style='float:right;margin:0px5px5px0px;'><br/>Total Rs:{product.Price}<br/><b>Thank you for shopping with us</b>" + "<br/><br/>Thanks&Regards</b><br/</p></body></html>";
                MyMessage.Label = "Your Order for the Product has been successfully placed";
                queue.Send(MyMessage);
                Message msg = queue.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                void SendInvoiceMail(string Email,ProductPostModel productPostModel)
                {
                    using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                    {
                        
                        client.EnableSsl = true;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = true;
                        client.Credentials = new NetworkCredential("testingshubhproject@gmail.com", "rpkygoynyppwwodj");
                        MailMessage msgObj = new MailMessage();
                        msgObj.To.Add(Email);
                        msgObj.IsBodyHtml = true;
                        
                        msgObj.From = new MailAddress("testingshubhproject@gmail.com");
                        msgObj.Subject = $"Your Order for {product.Name} HAS BEEN SUCCESSFULLY PLACED";
                        msgObj.Body = $"<html><body><p><p style='float:left;margin:0px 10px 10px 0px;'><b>Hi {user.Name}</br>,<br/>Your Order has been successfully placed.</p><p>Product Name {product.Name}<br/>Product ID {product.ProductId}</p><br/>Thankyou for shopping with our Product App<br/><img style='float:left;margin:0px 10px 10px 0px;'src='~/UploadedFiles/WhatsApp Image 2022-08-10 at 9.31.11 AM.jpeg' width='100' height='100'/><div style='float:right;margin:0px5px5px0px;'><br/>Total Rs:{product.Price}<br/><b>Thank you for shopping with us</b>" + "<br/><br/>Thanks&Regards</b><br/</p></body></html>";
                        MyMessage.Label = "Your Order for the Product has been successfully placed"; 
                        client.Send(msgObj);
                    }
                }
                SendInvoiceMail(user.Email, productPostModel);
                void msmqQueue_ReceivedCompleted(object sender,ReceiveCompletedEventArgs e)
                {
                    try
                    {
                        MessageQueue queue = (MessageQueue)sender;
                        Message msg = queue.EndReceive(e.AsyncResult);
                        SendInvoiceMail(e.Message.ToString(), productPostModel);
                        queue.BeginReceive();
                    }
                    catch(MessageQueueException ex)
                    {
                        if (ex.MessageQueueErrorCode == MessageQueueErrorCode.AccessDenied)
                        {
                            Console.WriteLine("Access is denied." + "Queue might be a system queue.");
                        }
                    }
                }
                queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceivedCompleted);
                queue.BeginReceive();
                queue.Close();  
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}



