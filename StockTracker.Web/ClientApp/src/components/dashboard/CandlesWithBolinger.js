import { Component } from "react";
import Chart from '../../charts/CandleStickChartWithBollingerBandOverlay'
import StockSymbolsList from '../shared/SymbolsList';


class chartWidgetWithBollinger extends Component{
    constructor(prop){
        super(prop);
    }

    selectedSymbol(ticker){
        console.log(ticker);
    }

    render(){
        return(
            <div>
                <StockSymbolsList callback={this.selectedSymbol} />
            </div>
        );
    }
}