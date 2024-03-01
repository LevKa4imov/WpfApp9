using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp9
{
    public partial class Сотрудники
    {
        public override string ToString()
        {

            // return base.ToString();
            return Familia;
        }
    }
    public partial class Специализация
    {
        public override string ToString()
        {

            // return base.ToString();
            return SpezializaziaSotrud;
        }
    }
    public partial class Клиент
    {
        public override string ToString()
        {

            // return base.ToString();
            return FamiliaZakazchika;

        }
    }
    public partial class Проекты

    {
        public override string ToString()
        {

            // return base.ToString();
            return  NazvanieProekta + "," + "стоимость" + " " + cena;
        }
    }
}
