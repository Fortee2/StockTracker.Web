import React, { Component } from 'react';
import SecuritiesForm from './Form';
import { connect } from 'react-redux';

const mapStateToProps = (state) => {
    return {
      listData: state.securities.securitiesData,
      editorVisibile: state.securities.editorVisible,
    };
  }

  class SecuritiesList extends Component{
    constructor(props) {
        super(props);
    
    }
    
    renderTable() {
        return (
          <div>
            <table className='table table-striped' aria-labelledby="tabelLabel">
              <thead>
                <tr>
                  <th>Symbol</th>
                  <th>Name</th>
                  <th>Industry</th>
                  <th>Sector</th>
                </tr>
              </thead>
              <tbody>
                {this.props.listData.map(
                    security => 
                    <tr key={security.id}>
                        <td>{security.symbol}</td>
                        <td>{security.name}</td>
                        <td>{security.industry}</td>
                        <td>{security.sector}</td>
                    </tr>
                )}
              </tbody>n
            </table>
          </div>
        );
      }


      render(){
          return(this.renderTable());
      }
}

export default connect(mapStateToProps)(SecuritiesList);