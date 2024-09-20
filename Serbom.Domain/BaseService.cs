using System;
using Serbom.Domain.Model;

namespace Serbom.Domain;

public class BaseService {
    
    protected User? _currentUser = null;
    protected IConfigurationRoot _configuration = null!;
    
    public BaseService()
    {
        setConfig();
    }

    public BaseService(string currentUserEmail)
    {
        if(!String.IsNullOrEmpty(currentUserEmail))
        {
            _currentUser = new UserService().Get(currentUserEmail);
        }

        setConfig();
    }

    public BaseService(User currentUser)
    {
        _currentUser = currentUser;

        setConfig();
    }

    private void setConfig()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
    }
}