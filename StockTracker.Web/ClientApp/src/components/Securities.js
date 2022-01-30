import React, { Component } from 'react';
import { connect } from 'react-redux'
import SecuritiesList from './securities/List';

const mapStateToProps = (state) => {
 // console.log(state.securities.isListLoading);
  const loading = state.securities.isListLoading
 // console.log('State of loading: ' + loading);
  return {
    isLoading:loading
  };
}

class Securities extends Component {
  static displayName = Securities.name;

  toggleLoadingState = () => {
    this.props.dispatch({type: 'securities/toogleLoading'});
  }

  updateSecuritiesData = (data) => {
      this.props.dispatch({type: 'securities/updateSecurities', payload: data});
  }

  addData = (data) => {
    return {
      type: 'securities/updateSecurities',
      payload: data
    }
  }

  constructor(props) {
    super(props);
    console.log(props);
  }

  componentDidMount() {
    this.populateData();
  }

  render() {
    console.log('this.props.isloading: ' + this.props.isLoading);
    let contents = this.props.isLoading
      ? <p><em>Loading...</em></p>
        :  <SecuritiesList />; 

    return (
      <div>

        <h1 id="tabelLabel" >Securites</h1>
        <p>Securities Tracked by the System</p>
        <button type="button" className="btn btn-secondary" disabled={this.props.isLoading}>Add</button>
        {contents}
      </div>
    );
  }

  async populateData() {
    const response = await fetch('Security');
    const data = await response.json();
    this.updateSecuritiesData(data);
    this.toggleLoadingState();
  }
}

export default connect(mapStateToProps)(Securities);