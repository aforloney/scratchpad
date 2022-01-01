using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Scratchpad.Examples
{
    internal class BuildPatternExample : IExample
    {
        private readonly ILogger _logger;

        public BuildPatternExample(ILogger<BuildPatternExample> logger)
        {
            _logger = logger;
        }

        public Task InvokeAsync()
        {
            Person person = PersonBuilder.Create()
                .WithName("Anthony")
                .WithPhoneNumber("1-401-569-2297")
                .HavingJob(builder => {
                    builder
                        .WithName("Software Engineer")
                        .WithAddress("500 Boylston Street, Boston MA, 02176");
                })
                .HavingJob(builder => {
                    builder
                        .WithName("Dog Dad")
                        .WithAddress("26 Pearl St, Quincy, MA 02169");
                })
                .Build();

            _logger.LogInformation(JsonSerializer.Serialize(person));
            return Task.CompletedTask;
        }
    }

    internal class PersonBuilder
    {   
        protected Person _person;
        private PersonBuilder()
        {
            _person = new Person();
        }

        // resonates with a Builder pattern (e.g, "Create" versus new'ing)
        public static PersonBuilder Create() => new PersonBuilder();

        // through the builder, set properties appropriately returning the instance for obj init chaining
        public PersonBuilder WithName(string name) {
            _person.Name = name;
            return this;
        }

        public PersonBuilder WithPhoneNumber(string phoneNumber) {
            _person.PhoneNumber = phoneNumber;
            return this;
        }

        // from the provided action of configuring the JobBuilder, setup the job
        //  builders should rely on other builders?
        public PersonBuilder HavingJob(Action<JobBuilder> configure)
        {
            var jobBuilder = new JobBuilder();
            configure(jobBuilder);
            _person.Jobs.Add(jobBuilder.Build());
            return this;
        }

        // when built, return back the constructed object
        public Person Build() => _person;
    }

    internal class Person
    {
        public string Name {get; set;}
        public string PhoneNumber {get; set;}
        public List<Job> Jobs {get; set;} = new List<Job>();
    }

    internal class JobBuilder
    {
        private readonly Job _job;

        public JobBuilder()
        {
            _job = new Job();
        }

        public JobBuilder WithName(string name) {
            _job.Name = name;
            return this;
        }

        public JobBuilder WithAddress(string address)
        {
            _job.Address = address;
            return this;
        }

        public Job Build() => _job;
    }

    internal record Job
    {
        public string Name {get; set;}
        public string Address {get; set;}
    }
}
