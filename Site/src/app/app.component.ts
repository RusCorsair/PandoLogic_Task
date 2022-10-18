import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';

import { ChartType } from 'angular-google-charts';
import { JobStatisticsService } from './_services/job-statistics.service';
import { CacheService } from './_services/cache.service';
import { Statistics } from './_models/statistics';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Cumulative job view vs prediction';

  type = ChartType.LineChart;

  data: any[] = [];

  columnNames = ['Date', 'JobViews', 'PredictedJobViews', 'ActiveJobs' ];

  options = {
    vAxis:{
       title: 'Job views total'
    },
  };

  width = 1024;

  height = 768;

  errorMessage = '';

  constructor(
    private statisticsService: JobStatisticsService,
    private datePipe: DatePipe,
    private cacheService: CacheService) {}

  ngOnInit(): void {
    this.clearData();
  }

  public isDataAvailable(){
    return this.data != null && this.data.length > 0;
  }

  public onDaterangeChange(dateRange: Date[]) {
    this.getJobStatistics(dateRange[0], dateRange[1]);
  }

  private getJobStatistics(startDate: Date, endDate: Date) {
    this.clearData();

    let cachedResult = this.cacheService.getCachedResult(startDate, endDate);
    if (cachedResult != null){
      this.fillData(cachedResult);
    } else {
      this.statisticsService.GetJobStatistics(startDate, endDate).subscribe((statistics) => {
        this.fillData(statistics);

        this.cacheService.cacheResult(startDate, endDate, statistics);
      }, error => {
        console.log(error);
        this.errorMessage = error?.message;
      });
    }
  }

  private fillData(statistics: Statistics[]){
    if (statistics.length > 0){
      statistics.forEach(row => {
        this.data.push([this.datePipe.transform(row.actualDate, 'mediumDate'), row.jobViews, row.predictedJobViews, row.activeJobs]);
      })
    } else {
      this.errorMessage = 'No data available for the selected time period.';
    }
  }

  private clearData() {
    this.errorMessage = '';
    this.data = [];
  }
}
