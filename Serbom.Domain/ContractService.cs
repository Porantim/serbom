using Serbom.Domain.Model;
using Serbom.Domain.Exception;

namespace Serbom.Domain
{
    public class ContractService : BaseService
    {
        public ContractService(string currentUserEmail) : base(currentUserEmail){}
    
        public int Insert(ContractData contractData)
        {
            using(var context = new SerbomContext()) {

                ClientService clientSrvc = new ClientService(_currentUser);
                if(contractData.Client.Id == 0)
                {
                    contractData.Contract.Client = clientSrvc.Insert(contractData.Client);
                } else
                {
                    clientSrvc.Update(contractData.Client);
                    contractData.Contract.Client = contractData.Client.Id;
                }
                context.Contracts.Add(contractData.Contract);
                context.SaveChanges();

                context.Histories.Add(new History
                {
                    Action = "create",
                    EntityType = "contract",
                    User = _currentUser.Id,
                    Date = DateTime.Now
                });

                return contractData.Contract.Id;
            }
        }

        public void Update(ContractData newContract)
        {
            using(var context = new SerbomContext()) {
                var oldContract = context.Contracts.Find(newContract.Contract.Id);
                if (oldContract == null)
                {
                    throw new NotFoundException();
                }
                oldContract.Client = newContract.Contract.Client;
                oldContract.Type = newContract.Contract.Type;
                oldContract.Number = newContract.Contract.Number;
                oldContract.Subject = newContract.Contract.Subject;
                oldContract.Start = newContract.Contract.Start;
                oldContract.End = newContract.Contract.End;
                oldContract.Value = newContract.Contract.Value;
                oldContract.Status = newContract.Contract.Status;
                oldContract.Conditions = newContract.Contract.Conditions;
                context.SaveChanges();

                ClientService clientSrvc = new ClientService(_currentUser);
                clientSrvc.Update(newContract.Client);
                newContract.Contract.Client = newContract.Client.Id;

                context.Histories.Add(new History
                {
                    Action = "update",
                    EntityType = "contract",
                    User = _currentUser.Id,
                    Date = DateTime.Now
                });
            }
        }

        public ContractData Get(int id) {
            using(var context = new SerbomContext()) {
                var contract = context.Contracts.Find(id);
                if (contract == null)
                {
                    throw new NotFoundException();
                }
                var client = context.Clients.Find(contract.Client);
                return new ContractData
                {
                    Contract = contract,
                    Client = client
                };
            }
        }

        public Object[] List()
        {
            using(var context = new SerbomContext()) {

                var list = from c in context.Contracts
                           from cl in context.Clients
                           from t in context.ContractTypes
                           from s in context.ContractStatuses
                           where c.Client == cl.Id
                              && c.Type == t.Id
                              && c.Status == s.Id
                           orderby c.Id descending
                           select new {
                                id = c.Id,
                                number = c.Number,
                                subject = c.Subject,
                                start = c.Start,
                                value = c.Value,
                                type = t.Name,
                                status = s.Description,
                                client = new
                                {
                                    id = cl.Id,
                                    name = cl.Name,
                                    city = cl.City,
                                    state = cl.State
                                },
                           };
                return list.ToArray<Object>();
            }
        }

        public List<ContractType> ListTypes()
        {
            using(var context = new SerbomContext()) {
                return context.ContractTypes.OrderBy(c => c.Name).ToList();
            }
        }

        public List<ContractStatus> ListStatuses()
        {
            using(var context = new SerbomContext()) {
                return context.ContractStatuses.OrderBy(c => c.Description).ToList();
            }
        }
    }

    public class ContractData { 
        public Contract Contract {get; set;}
        public Client Client {get; set;}
    }

}