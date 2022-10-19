import React, {Component, Fragment} from "react";
import { Chart } from "react-google-charts";
import StockSymbolsList from '../components/shared/SymbolsList'


export const options = {
    legend: "none",
    bar: { groupWidth: "100%" }, // Remove space between bars.
    displayZoomButtons: true,
    candlestick: {
      fallingColor: { strokeWidth: 0, fill: "#a52714" }, // red
      risingColor: { strokeWidth: 0, fill: "#0f9d58" }, // green
    },
  };

export class CandleStickChart extends Component{

    constructor(props){
        super(props);
        this.state = {chartData:[]};
    }

    componentDidMount(){

    }

    selectedSymbol(ticker){
        console.log('Ticker ID in callback: ' + ticker);
        this.retrieveData(ticker);
    }
    
    objectToArray = obj => {
        const keys = Object.keys(obj);
        const res = [];
        for(let i = 0; i < keys.length; i++){
           res.push(obj[keys[i]]);
        };
        return res;
     };

    retrieveData(ticker){
        fetch('api/candlestick/' + ticker)
            .then( response => {if (!response.ok) {
                throw new Error('Network response was not OK');
              }
              return response.json();
            })
            .then(data => {
                let collector = [["Day","","","",""]];

                data.map(a => { 
                    let line = [];
                    line.push(a.date);
                    line.push(a.low);
                    line.push(a.open);
                    line.push(a.close);
                    line.push(a.high);

                    collector.push(line);
                }  );
                console.log(collector);
                this.setState({chartData: collector});  
            } )
            .catch(err => console.log(err))
        ;
    }

    render(){
        let chartBody = <span>No Data</span>;
        if(this.state.chartData.length  > 0){
            chartBody = <Chart
                chartType="CandlestickChart"
                width="1000px"
                height="50%"
                data={this.state.chartData}
                options={options}
            />
            }

        return(


            <Fragment>
                <StockSymbolsList callback={(e) => this.selectedSymbol(e)}  />
                {chartBody}
            </Fragment>

        )
    }

}