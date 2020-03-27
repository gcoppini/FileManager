import React, { Component } from 'react';
import axios from 'axios';
import {Progress} from 'reactstrap';
import logo from './logo.svg';
import './App.css';

class App extends Component {

  constructor(props) {
    super(props);
      this.state = {
        selectedFile: null,
        loaded:0,
        files: []
      }
  }

  


  componentDidMount() {
    this.loadFiles();
  }
  
  render() {
    return (
      <div className="App">
        <header className="App-header">
        <h2>File Transfer App</h2>
          <div>
            <p>Send Files</p>
            <input type="file" name="file" className="form-control"  onChange={this.onChangeHandler}/>
            <div className="form-group">
              <Progress max="100" color="success" value={this.state.loaded} >{Math.round(this.state.loaded,2) }%</Progress>
            </div>
            <button type="button" className="btn btn-success btn-block" onClick={this.onClickHandler}>Upload</button> 
            
            <h2>Stored Files</h2>
            <ul className="list-group">
              { 
                this.state.files.map(file => <li className="list-group-item d-flex justify-content-between align-items-center" key={file.id}><span className="badge badge-success" onClick={() => this.onDownloadHandler(file.filename)}>{file.filename}</span>&nbsp;-<span className="badge badge-warning badge-pill">{file.metadata.contentType}</span>-&nbsp;<span className="badge badge-primary badge-pill" onClick={() => this.onDeleteHandler(file.filename)}>Delete</span></li>)
              }
            </ul>
          </div>
        </header>
        <main>
        <img src={logo} className="App-logo" alt="logo" />
          <p>
            Gabriel Abner <code>Coppini</code>
          </p>
          <a
            className="App-link"
            href="https://gcoppini.github.io"
            target="_blank"
            rel="noopener noreferrer">
            Mais sobre mim
          </a>
   
        </main>
      </div>
    );
  }

  onDeleteHandler = (fileName) => {
    console.log("Delete callded - "+ fileName);
    
    axios.get("http://localhost:5000/api/FileStorage/remove?filename="+fileName).then(res => {
        console.log(res.statusText)
        this.loadFiles();
      })
  }

  onDownloadHandler = (fileName) =>{
    console.log("Download callded - "+fileName);

    var url = "http://localhost:5000/api/FileStorage/download?filename="+fileName;
    window.location.assign(url);
    
    //axios.get("http://localhost:5000/api/FileStorage/download?filename="+fileName).then(res => {
     //   console.log(res.statusText)
        //this.loadFiles();
     // })
  }


  onChangeHandler=event=>{

    console.log(event.target.files[0])

    this.setState({
      selectedFile: event.target.files[0],
      loaded: 0,
    })
  }

  onClickHandler = () => {
    const data = new FormData()
    data.append('file', this.state.selectedFile)

    axios.post("http://localhost:5000/api/FileStorage/upload", data, { 
      // receive two    parameter endpoint url ,form data
        onUploadProgress: ProgressEvent => {
        this.setState({
          loaded: (ProgressEvent.loaded / ProgressEvent.total*100),
        })},
      
    }).then(res => { // then print response status
      console.log(res.statusText)
      this.loadFiles();
    })
  }

  loadFiles = () => {
    axios.get("http://0.0.0.0:5000/api/FileStorage/").then(res => {
        const files = res.data;
        this.setState({ files });
      })
  }

  

  

}

export default App;

