import React, { Component } from 'react';

export default class SecuritiesForm extends Component{
     constructor(props) {
        super(props);
        this.state = { securities: [], loading: true };
      }


    render(){
      return (
        <form>
          <div class="form-row">
            <div class="form-group col">
              <label for="inputSecuritySymbol">Symbol</label>
              <input type="text" id="inputSecuritySymbol" class="form-control" placeholder="Security Symbol"/>
            </div>
            <div class="col">
              <label for="inputSecurityName">Security Name</label>
              <input type="text" id="inputSecurityName" class="form-control" placeholder="Security Name"/>
            </div>
          </div>

        </form>
      );
    }
}