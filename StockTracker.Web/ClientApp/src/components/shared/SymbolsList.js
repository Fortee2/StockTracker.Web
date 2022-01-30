import { Component } from "react";

export default class StockSymbolsList extends Component{

    constructor(props){
        super(props);
        this.state({symbols:[{synbol:'msft', name:'Microsoft'}, {symbol:'cmx', name:'Carmax'}]})
    }

    render(){
        return(
            <select>
                {this.state.symbols.map((symbol)=>{
                    return <option value={symbol.symbol}>{symbol.name}</option>
                })}
            </select>
        );
    }
}