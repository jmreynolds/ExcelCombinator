using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.EqualityImplementations;
using Core.Models;
using CODE.Framework.Core.Utilities;

namespace DataAccess
{
    public class ProcessMunicipalItems : IProcessMunicipalItems
    {
        private int _citations;
        private int _rowsToWrite;

        public int Citations
        {
            get { return _citations; }
            set
            {
                _citations = value;
                CitationsCountChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler CitationsCountChanged;

        public IEnumerable<CashBondForfitureInput> MapToCashBondForfitureInput(
            Dictionary<int, IEnumerable<RowItem>> origin)
        {
            var foo = origin.Values
                .Select(items =>
                {
                    var rowItems = items as RowItem[] ?? items.ToArray();
                    return rowItems;
                })
                .ToList();
            foreach (var rowArrays in foo)
            {
                var result = new CashBondForfitureInput();
                foreach (var rowItem in rowArrays)
                {
                    var columnName = rowItem.ColumnName;
                    var rowValue = rowItem.Value;
                    DynamicItem item;
                    switch (columnName)
                    {
                        case "Violation Date":
                            result.DynamicItems.Find(x => x.ColumnName == "OffenseDate").Value = rowValue;
                            break;
                        case "Citation":
                            item = result.DynamicItems.Find(x => x.ColumnName == "CitationNumber");
                            item
                                .Value = rowValue;
                            break;
                        case "Defendant":
                            item = result.DynamicItems.Find(x => x.ColumnName == "Name");
                            item.Value =rowValue;
                            item.ShouldRemoveDupes = true;
                            item.ShouldIncludeInOutput = true;
                            break;
                        case "Address":
                            item = result.DynamicItems.Find(x => x.ColumnName == "Address");
                            item.Value = rowValue;
                            item.ShouldRemoveDupes = true;
                            item.ShouldIncludeInOutput = true;
                            break;
                        case "City":
                            item = result.DynamicItems.Find(x => x.ColumnName == "City");
                            item.Value = rowValue;
                            item.ShouldRemoveDupes = true;
                            item.ShouldIncludeInOutput = true;
                            break;
                        case "State":
                            item = result.DynamicItems.Find(x => x.ColumnName == "State");
                            item.Value = rowValue;
                            item.ShouldRemoveDupes = true;
                            item.ShouldIncludeInOutput = true;
                            break;
                        case "Zip":
                            item = result.DynamicItems.Find(x => x.ColumnName == "Zip");
                            item.Value = rowValue;
                            item.ShouldRemoveDupes = true;
                            item.ShouldIncludeInOutput = true;
                            break;
                        case "Charge":
                            item = result.DynamicItems.Find(x => x.ColumnName == "Offense");
                            item.Value = rowValue;
                            break;
                        case "Disposition Date":
                            item = result.DynamicItems.Find(x => x.ColumnName == "DispositionDate");
                            item.Value = rowValue;
                            // item.ColumnName = "DispositionDate";
                            item.ShouldIncludeInOutput = true;
                            break;
                    }
                }
                yield return result;
            }
        }


        public IEnumerable<CashBondForfitureOutput> MapToCashBondForfitureOutput(
            IEnumerable<CashBondForfitureInput> input)
        {

            Citations = 0;
            var comparer = new DynamicOutputCompare();
            var rows = input.ToList();
            var result = new List<CashBondForfitureOutput>();

            foreach (var row in rows)
            {
                var newItem = new CashBondForfitureOutput {DynamicItems = row.DynamicItems.MapFields()};
                // map fields
                // dedupe

                var existingItem = result.FirstOrDefault(x => comparer.Equals(x, newItem) 
                                                            && x.DynamicItems.Count(y => y.ColumnName=="Citations") < 7);

                if (existingItem != null 
                    && existingItem.DynamicItems
                        .First(x=>x.ColumnName=="Citations")
                        .Value.Count <7 )
                {
                    existingItem.DynamicItems
                                .First(x => x.ColumnName == "Citations")
                                .Value
                                .Add(new Citation
                                {
                                    CitationNumber =
                                        row.DynamicItems.Single(y => y.ColumnName == "CitationNumber").Value,
                                    Offense = row.DynamicItems.Single(y => y.ColumnName == "Offense").Value
                                });
                }
                else
                {
                    newItem.DynamicItems.Add(new DynamicItem()
                    {
                        ColumnName = "Citations",
                        ShouldRemoveDupes = false,
                        ShouldAggregate = false,
                        ShouldIncludeInOutput = true,
                        Value = new List<Citation> {
                            new Citation {
                                CitationNumber = row.DynamicItems.Single(y => y.ColumnName == "CitationNumber").Value,
                                Offense = row.DynamicItems.Single(y => y.ColumnName == "Offense").Value
                            }
                        }
                    });
                    result.Add(newItem);
                }
            }
            var citationList = result
                .SelectMany(x => x.DynamicItems.Where(y=>y.ColumnName=="Citations"), 
                    (x, y) => y.Value)
                .OfType<List<Citation>>()
                .SelectMany(x=>x, (x,y)=> y);
            Citations = citationList.Count();
            RowsToWrite = result.Count;
            return result;
        }

        public int RowsToWrite
        {
            get { return _rowsToWrite; }
            private set
            {
                _rowsToWrite = value;
                RowsToWriteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler RowsToWriteChanged;
        public IEnumerable<DynamicInput> MapDynamicInput(Dictionary<int, IEnumerable<RowItem>> origin)
        {
            var result = MapToCashBondForfitureInput(origin);
            return result;
        }

        public IEnumerable<DynamicOutput> MapDynamicInputToDynamicOutput(IEnumerable<DynamicInput> input)
        {
            var cashBondForfitureInputs = new List<CashBondForfitureInput>();
            input.ToList().ForEach(x =>
            {
                var cbfi = new CashBondForfitureInput(x.DynamicItems);
                cashBondForfitureInputs.Add(cbfi);
            });
            return MapToCashBondForfitureOutput(cashBondForfitureInputs);
        }
    }
}