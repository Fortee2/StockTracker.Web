import React, { Component, useState } from "react";

export default class StockSymbolsList extends Component{

    constructor(props){
        super(props);

        this.state = { symbolList: [] };
    }

    componentDidMount() {
        this.retrieveSymbols();
    }

    retrieveSymbols() {
        fetch('api/security/')
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not OK');
                }
                return response.json();
            })
            .then(data => {
                let collector = [];

                data.map(a => {
                    collector.push({ symbol: a.symbol, name: a.name, value: a.id });
                });
                console.log(collector);
                this.setState({ symbolList: collector });
            })
            .catch(err => console.log(err))
            ;

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
                {this.state.symbolList.map((symbol)=>{
                    return <option key={symbol.symbol} value={symbol.value}>{symbol.symbol} - {symbol.name}</option>
                })}
            </select>
        );
    }
}