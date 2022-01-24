import React, { Component } from 'react';
import { Routes, Route } from "react-router-dom";
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
        <Layout>
            <Routes>
                <Route exact path='/' component={Home} element={<Home />} />
                <Route path='/counter' component={Counter} element={<Counter />} />
                <Route path='/fetch-data' component={FetchData} element={<FetchData />} />
            </Routes>
      </Layout>
    );
  }
}
