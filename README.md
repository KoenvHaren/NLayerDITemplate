# Intro

Every programmer uses his or her own architectural contraints in one way or another. This is a sample application on the best practices I learned of while applying an N-layered architectural pattern to a clean application with excellent enforcement of separation of concerns.


A quick template for using .Net Core Constructor injection with an N-layered architecture. 

Please find ServiceMapper.cs in your Service layer for registering services and interfaces in your Service layer or 'higher'.

# Usage 

This project has a template file included with which you can generate the architectural pattern found in this project. Alternatively, the app can be cloned. 

Add Template/NLayerDITemplate.zip to $user$\Documents\Visual Studio $version$\Templates\ProjectTemplates to use the template. You can then find the template in new project-> Installed -> VisualC# (second one).

# Best practices?

In the intro I mention a few best practices I have learned through the years, working with .net Core. A couple which can be found in this app:

### ServiceMapper.cs
The ServiceMapper.cs class has an answer to the nagging realisation that if you use a standard DI-container like the one found in .Net Core apps, all of a sudden your N-layered architecture is broken by default. View the following sample:

![N-Layer Image](https://www.codeproject.com/KB/aspnet/xenta/arch.png)

What happens if I want to register the Interfaces pertaining to the Data Access Layer? By default a Web App is its own presentation layer. I now have to add a reference to my DAL, in the Presentation layer, violating the N-Layered architectural pattern on a project level.

The ServiceMapper.cs class goes circumvents this issue by extending the DI-service container and the configuration files(!) of the presentation layered WebApp to the .Service layer. This way, the DAL is still only aware the Service layer exists, ensuring no dependency violations occur. 

```c#
public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //DbContext
            services.AddDbContext()

            //Services Services for a list on Service lifetimes, Scoped vs Transient etc. see https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2#service-lifetimes
            services.AddTransient<IValueService, ValueService>();

            //Repositories
            services.AddScoped<IValueRepository, ValueRepository>();

            //HttpClients https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.2#typed-clients
            services.AddHttpClient<IValueService, ValueService>();
        }
```

This has the side advantage of excellent extendability: What if you want to add a WebApi to that snazzy WebApp you're already running. In the backend a lot of the CRUD operations will be the same right? ServiceMapper allows you to just register all your .Service and .DAL interfaces and implementation in a single location, independant of your presentation layer.

```c#
// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Register ServiceMapper
            ServiceMapper.ConfigureServices(services, Configuration);
        }
```

### Typed HttpClients 
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.2#typed-clients
Not exactly a secret to most developers familiar with .net Core already, but those migrating from .Net, might be surprised with how the HttpClient class functions in a DI enviroment. Every time a requests initialises another HttpClient class, it opens another Port on your server. Now this would be fine, because requests are finished right? Wrong. The sockets are kept open for about 2-5 minutes, just waiting in case another response is given to the earlier request. This can get out of hand on busy Apps fast. 

Just using a single (static) HttpClient seems to be the solution, but brings a lot of logistical difficulties with it when you start going into different layers of your application. What if i use an external provider for Auth? I need a connection in my Presentation layer for that middleware connection, but then my external loggingservice would also require one in the Business Logic layer.

This issue has been solved using Typed HttpClients, using the default DI system in .net Core. It allows you to inject a specific HttpClient instance each time a particular service is called on. 

```c#
...
//Services
services.AddTransient<IValueService, ValueService>();

//Typed HttpClients
services.AddHttpClient<IValueService, ValueService>(c => 
            {
                c.BaseAddress = new Uri(configuration.GetSection("UrlSettings:ValueUrl").Key);
                c.DefaultRequestHeaders.Add("User-Agent", "NLayerTemplateSample");
            });
...
```

Now for everytime my IValueService is called on, and the ValueService implementation gets injected, I get a particular HttpClient using its old ports, pre-configured to be aimed at a certain Url, using some default headers. Neat huh?
