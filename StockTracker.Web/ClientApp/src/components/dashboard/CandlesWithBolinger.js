import { Component } from "react";
import Chart from '../../charts/CandleStickChartWithBollingerBandOverlay'
import StockSymbolsList from '../shared/SymbolsList';


export class ChartWidgetWithBollinger extends Component{
    constructor(prop){
        super(prop);
    }

    selectedSymbol(ticker){
        console.log(ticker);
    }

    retrieveData(ticker){
        const response =  fetch('api/Activities/' + ticker );
        const data =  response.json();
    }

    render(){
        return(
            <div>
                <StockSymbolsList callback={this.selectedSymbol}  />
                {/* <Chart data={[]} type='hybrid' /> */}
            </div>
        );
    }
}