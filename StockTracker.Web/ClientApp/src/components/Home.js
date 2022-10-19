import React, { Component } from 'react';
import { CandleStickChart } from '../charts/CandleStick';


export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <CandleStickChart />
      </div>
    );
  }
}
