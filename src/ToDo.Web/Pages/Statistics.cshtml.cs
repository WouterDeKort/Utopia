using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;

namespace ToDo.Web.Pages
{
    public class StatisticsModel : PageBaseModel
    {
        public int TotalCount { get; set; }

        public int PeopleCount { get; set; }

        public int BigTaskCount { get; set; }

        public int SmallTaskCount { get; set; }

        public long AvarageHours { get; set; }

        public long AvarageHoursPerPerson { get; set; }

        public List<DateStatic> NextMonthStatistic { get; set; }
        
        public StatisticsModel(
            IRepository repository,
            IFeatureToggleRepository featureToggleRepository,
            IApplicationMonitor applicationMonitor)
            : base(repository, featureToggleRepository, applicationMonitor)
        {
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!_featureToggleRepository.StatisicsIsEnabled())
            {
                return NotFound();
            }

            var items = await _repository.ListAsync<ToDoItem>();
            var nextMonthEndDate = DateTime.Now.AddMonths(1);

            TotalCount = items.Count;
            PeopleCount = items.Select(i => i.Owner).Distinct().Count();
            BigTaskCount = items.Count(i => i.Hours > 100);
            SmallTaskCount = items.Count(i => i.Hours < 25);
            AvarageHours = items.Sum(i => i.Hours.Value) / items.Count;
            AvarageHoursPerPerson = items.Sum(i => i.Hours.Value) / items.Select(i => i.Owner).Distinct().Count();
            NextMonthStatistic = items.Where(i => i.DueDate > DateTime.Now && i.DueDate < nextMonthEndDate)
                                      .Select(i => new DateStatic { Date = i.DueDate, Hours = i.Hours.Value }).ToList();

            return Page();
        }
    }

    public class DateStatic
    {
        public DateTime Date { get; set; }

        public long Hours { get; set; }
    }
}