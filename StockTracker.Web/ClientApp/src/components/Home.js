import React, { Component } from 'react';
import {ChartWidgetWithBollinger} from './dashboard/CandlesWithBolinger';


export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <ChartWidgetWithBollinger />
      </div>
    );
  }
}
