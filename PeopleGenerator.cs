using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.dane.gov.pl;

namespace Generator_pliku_ddd
{
    class PeopleGenerator
    {
        private ClientApi _api;
        private Random _rand;

        private PeopleGenerator()
        {
            this._api = new ClientApi();
            this._rand = new Random();
        }

        public async static Task<List<Person>> GeneretePeple(uint amount, double frequency)
        {

            var generator = new PeopleGenerator();

            var manFreaq = (uint)(amount * frequency);
            var womanFreaq = amount - manFreaq;

            var menNames = await generator._api.GetMenNames(manFreaq);
            var menLastnames = await generator._api.GetMenLastnames(manFreaq);

            var womenNames = await generator._api.GetWomenNames(womanFreaq);
            var womenLastnames = await generator._api.GetWomenLastnames(womanFreaq);

            return generator.Mix(menNames, menLastnames, womenNames, womenLastnames);
        }

        private List<Person> Mix (List<string> m_name, List<string> m_lastname, List<string> w_name, List<string> w_lastname)
        {
            var men = _createPeople(m_name, m_lastname);
            var women = _createPeople(w_name, w_lastname);

            var count = men.Count + women.Count;
            var people = new List<Person>();

            for(int i = 0; i < count; i++)
            {
                var idx = _rand.Next(men.Count + women.Count -1);
 
                if (idx >= men.Count)
                {
                    idx -= men.Count;

                    people.Add(women[idx]);
                    women.RemoveAt(idx);
                }
                else
                {
                    people.Add(men[idx]);
                    men.RemoveAt(idx);

                }
            }
            return people;
        }

        private List<Person> _createPeople(List<string> name, List<string> lastname)
        {
            var people = new List<Person>();

            var count = name.Count;

            for(int i = 0; i < count; i++)
            {
                var n_idx = name.Count > 1 ? _rand.Next(name.Count - 1) : 0;
                var l_idx = lastname.Count > 1 ? _rand.Next(lastname.Count - 1) : 0;

                people.Add(new Person(name[n_idx], lastname[l_idx], _rand));
           
                name.RemoveAt(n_idx);
                lastname.RemoveAt(l_idx);
            }

            return people;
        }
    }
}
