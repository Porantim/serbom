using System;
using Serbom.Domain.Model;
using Serbom.Domain.Exception;

namespace Serbom.Domain;

public class ClientService : BaseService
{
    public ClientService(string currentUserEmail) : base(currentUserEmail){}

    public ClientService(User currentUser) : base(currentUser){}
    
    public int Insert(Client client)
    {
        using(var context = new SerbomContext()) {

            context.Clients.Add(client);
            context.SaveChanges();

            context.Histories.Add(new History
            {
                Action = "create",
                EntityType = "client",
                User = _currentUser.Id,
                Date = DateTime.Now
            });

            return client.Id;
        }
    }

    public void Update(Client client)
    {
        using(var context = new SerbomContext()) {
            var oldClient = context.Clients.Find(client.Id);
            if (oldClient == null)
            {
                throw new NotFoundException();
            }
            oldClient.Name = client.Name;
            oldClient.Type = client.Type;
            oldClient.Email = client.Email;
            oldClient.Phone = client.Phone;
            oldClient.ZipCode = client.ZipCode;
            oldClient.State = client.State;
            oldClient.City = client.City;
            oldClient.Address1 = client.Address1;
            oldClient.Address2 = client.Address2;
            context.SaveChanges();

            context.Histories.Add(new History
            {
                Action = "update",
                EntityType = "client",
                User = _currentUser.Id,
                Date = DateTime.Now
            });
        }
        
    }

    public List<Client> List()
    {
        using(var context = new SerbomContext()) {
            return context.Clients.ToList();
        }
    }

    public Client Get(int id)
    {
        using(var context = new SerbomContext()) {
            var client = context.Clients.Find(id);
            if (client == null)
            {
                throw new NotFoundException();
            }
            return client;
        }
    }

    public Client GetByDocument(string document)
    {
        using (var context = new SerbomContext())
        {
            var client = context.Clients.FirstOrDefault(c => c.Document == document);
            if (client == null)
            {
                throw new NotFoundException();
            }
            return client;
        }
    }

    public List<Contract> GetContracts(int id)
    {
        using(var context = new SerbomContext()) {
            return context.Contracts.Where(c => c.Client == id).ToList();
        }
    }

}
