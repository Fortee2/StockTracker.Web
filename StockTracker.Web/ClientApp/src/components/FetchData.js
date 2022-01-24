import React, { Component } from 'react';

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { securities: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

    static renderForecastsTable(securities) {
    return (
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
            {securities.map(security =>
            <tr key={security.id}>
                <td>{security.symbol}</td>
                <td>{security.name}</td>
                <td>{security.industry}</td>
                <td>{security.sector}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : FetchData.renderForecastsTable(this.state.securities);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const response = await fetch('Securities');
    const data = await response.json();
    this.setState({ securities: data, loading: false });
  }
}
