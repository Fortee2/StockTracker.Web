import React, { Component } from 'react';
import { Routes, Route } from "react-router-dom";
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import Securities  from './components/Securities';
import { Counter } from './components/Counter';
import store from './app/store';
import { Provider } from 'react-redux';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Provider store={store}>
        <Layout>
            <Routes>
                <Route exact path='/' component={Home} element={<Home />} />
                <Route path='/counter' component={Counter} element={<Counter />} />
                <Route path='/securities' component={Securities} element={<Securities />} />
            </Routes>
        </Layout>
      </Provider>
    );
  }
}
