using FitLife.Logic.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FitLife.Logic.ViewModels
{
    public class VMWeightChart : INotifyPropertyChanged
    {
        public List<Weight> _weight { get; set; }
        public List<Macro> _macro { get; set; }

        public List<Weight> Weight
        {
            get => _weight;
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<Macro> Macro
        {
            get => _macro;
            set
            {
                if (_macro != value)
                {
                    _macro = value;
                    OnPropertyChanged();
                }
            }
        }

        public VMWeightChart()
        {
            Weight = new List<Weight>();
            Macro = new List<Macro>();
        }


        public void UpdateWeightChart(List<Weight> newWeight)
        {
            Weight = newWeight;
        }

        public void UpdateMacroChart(List<Macro> newMacro)
        {
            Macro = newMacro;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
