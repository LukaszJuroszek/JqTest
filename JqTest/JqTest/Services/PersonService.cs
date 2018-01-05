using JqTest.DAL;
using JqTest.Models;
using JqTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JqTest.Services
{
    public class PersonService
    {
        private Repository<Person> _personRepository;

        public PersonService(JqContext context)
        {
            _personRepository = new Repository<Person>(context);
        }

        public Person GetPersonById(int id)
        {
            return _personRepository.Find(x => x.Id == id);
        }

        public List<PersonViewModel> GetAllPersons()
        {
            return _personRepository.All().ToList().Select(x =>
            new PersonViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                SecondName = x.SecondName,
                CreatedAt = x.CreatedAt.ToShortDateString()
            }).ToList();
        }

        public bool UpdatePerson(Person model)
        {
            var person = GetPersonById(model.Id);
            if (person != null)
            {
                try
                {
                    person.FirstName = model.FirstName;
                    person.SecondName = model.SecondName;
                    _personRepository.Update(person);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
                return false;
        }

        public bool DeletePerson(int id)
        {
            var person = _personRepository.Find(x => x.Id == id);
            if (person != null)
            {
                try
                {
                    _personRepository.Delete(person);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool AddPerson(Person person)
        {
            person.CreatedAt = DateTime.Now;
            try
            {
                _personRepository.Add(person);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}