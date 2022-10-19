import React, { Component } from "react";

export default class StockSymbolsList extends Component{

    constructor(props){
        super(props);
    }

    returnSymbols(){
        return [{synbol:'msft', name:'Microsoft', value: 1}, {symbol:'cmx', name:'Carmax', value: 3}];
    }

    symbolHasChanged(event, callback){
        console.log(event.target.selectedOptions[0].value);
        if(callback != null){
            callback(event.target.selectedOptions[0].value);
        }
    }

    render(){
        return(
            <select onChange={e => this.symbolHasChanged(e,this.props.callback)}>
                {this.returnSymbols().map((symbol)=>{
                    return <option key={symbol.symbol} value={symbol.value}>{symbol.name}</option>
                })}
            </select>
        );
    }
}