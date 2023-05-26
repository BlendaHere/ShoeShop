
    import React, { Component } from 'react';
    import { variables } from '../Variables.js';
    
    export class Product extends Component {
      constructor(props) {
        super(props);
    
        this.state = {
          products: [],
          modalTitle: "",
          ProductId: 0,
          ProductName: "",
          Brand: "",
          Color: "",
          Size: 0,
          Price: 0,
          Quantity: 0,
          ImageName: "addidas.png",
          ImagePath: variables.IMAGE_URL,
        };
      }
    
      refreshList() {
        fetch(variables.API_URL + 'product')
          .then((response) => response.json())
          .then((data) => {
            this.setState({ products: data });
          });
      }
    
      componentDidMount() {
        this.refreshList();
      }
    
      changeProductName = (e) => {
        this.setState({ ProductName: e.target.value });
      };
    
      changeBrand = (e) => {
        this.setState({ Brand: e.target.value });
      };
    
      changeColor = (e) => {
        this.setState({ Color: e.target.value });
      };
    
      changeSize = (e) => {
        this.setState({ Size: e.target.value });
      };
    
      changePrice = (e) => {
        this.setState({ Price: e.target.value });
      };
    
      changeQuantity = (e) => {
        this.setState({ Quantity: e.target.value });
      };
    
      addClick() {
        this.setState({
          modalTitle: "Add Product",
          ProductId: 0,
          ProductName: "",
          Brand: "",
          Color: "",
          Size: 0,
          Price: 0,
          Quantity: 0,
          ImageName: "addidas.png",
        });
      }
    
      editClick(prod) {
        this.setState({
          modalTitle: "Edit Product",
          ProductId: prod.ProductId,
          ProductName: prod.ProductName,
          Brand: prod.Brand,
          Color: prod.Color,
          Size: prod.Size,
          Price: prod.Price,
          Quantity: prod.Quantity,
          ImageName: prod.ImageName,
        });
      }
    
      createClick() {
        fetch(variables.API_URL + 'product', {
          method: 'POST',
          headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            ProductName: this.state.ProductName,
            Brand: this.state.Brand,
            Color: this.state.Color,
            Size: this.state.Size,
            Price: this.state.Price,
            Quantity: this.state.Quantity,
            ImageName: this.state.ImageName,
          }),
        })
          .then((res) => res.json())
          .then(
            (result) => {
              alert(result);
              this.refreshList();
            },
            (error) => {
              alert('Failed');
            }
          );
      }
    
      updateClick() {
        fetch(variables.API_URL + 'product', {
          method: 'PUT',
          headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
            ProductId: this.state.ProductId,
            ProductName: this.state.ProductName,
            Brand: this.state.Brand,
            Color: this.state.Color,
            Size: this.state.Size,
            Price: this.state.Price,
            Quantity: this.state.Quantity,
            ImageName: this.state.ImageName,
          }),
        })
          .then((res) => res.json())
          .then(
            (result) => {
              alert(result);
              this.refreshList();
            },
            (error) => {
              alert('Failed');
            }
          );
      }
    
      deleteClick(id) {
        if (window.confirm('Are you sure?')) {
          fetch(variables.API_URL + 'product/' + id, {
            method: 'DELETE',
            headers: {
              Accept: 'application/json',
              'Content-Type': 'application/json',
            },
          })
            .then((res) => res.json())
            .then(
              (result) => {
                alert(result);
                this.refreshList();
              },
              (error) => {
                alert('Failed');
              }
            );
        }
      }
    
      imageUpload = (e) => {
        e.preventDefault();
    
        const formData = new FormData();
        formData.append('file', e.target.files[0], e.target.files[0].name);
    
        fetch(variables.API_URL + 'product/savefile', {
          method: 'POST',
          body: formData,
        })
          .then((res) => res.json())
          .then((data) => {
            this.setState({ ImageName: data });
          });
      };
    
      render() {
        const {
          products,
          modalTitle,
          ProductId,
          ProductName,
          Brand,
          Color,
          Size,
          Price,
          Quantity,
          ImagePath,
          ImageName,
        } = this.state;
    
        return (
            <div>
              <button
                type="button"
                className="btn btn-primary m-2 float-end"
                data-bs-toggle="modal"
                data-bs-target="#exampleModal"
                onClick={() => this.addClick()}
              >
                Add Product
              </button>
              <table className="table table-striped">
                <thead>
                  <tr>
                    <th>ProductId</th>
                    <th>ProductName</th>
                    <th>Brand</th>
                    <th>Color</th>
                    <th>Size</th>
                    <th>Price</th>
                    <th>Quantity</th>
                    <th>Options</th>
                  </tr>
                </thead>
                <tbody>
                  {products.map((prod) => (
                    <tr key={prod.ProductId}>
                      <td>{prod.ProductId}</td>
                      <td>{prod.ProductName}</td>
                      <td>{prod.Brand}</td>
                      <td>{prod.Color}</td>
                      <td>{prod.Size}</td>
                      <td>{prod.Price}</td>
                      <td>{prod.Quantity}</td>
                      <td>
                        <button
                          type="button"
                          className="btn btn-light mr-1"
                          data-bs-toggle="modal"
                          data-bs-target="#exampleModal"
                          onClick={() => this.editClick(prod)}
                        >
                          <svg
                            xmlns="http://www.w3.org/2000/svg"
                            width="16"
                            height="16"
                            fill="currentColor"
                            className="bi bi-pencil-square"
                            viewBox="0 0 16 16"
                          >
                            <path
                              d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"
                            />
                            <path
                              fillRule="evenodd"
                              d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0"
                            />
                          </svg>
                        </button>
                        <button
                          type="button"
                          className="btn btn-danger"
                          onClick={() => this.deleteClick(prod.ProductId)}
                        >
                          Delete
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
        
              <div
                className="modal fade"
                id="exampleModal"
                tabIndex="-1"
                aria-labelledby="exampleModalLabel"
                aria-hidden="true"
              >
                <div className="modal-dialog">
                  <div className="modal-content">
                    <div className="modal-header">
                      <h5 className="modal-title">{modalTitle}</h5>
                      <button
                        type="button"
                        className="btn-close"
                        data-bs-dismiss="modal"
                        aria-label="Close"
                      ></button>
                    </div>
                    <div className="modal-body">
                      <div className="input-group mb-3">
                        <span className="input-group-text">ProductId</span>
                        <input
                          type="text"
                          className="form-control"
                          value={ProductId}
                          onChange={ this.changeProductId}
                        />
                      </div>
                      <div className="input-group mb-3">
                        <span className="input-group-text">ProductName</span>
                        <input
                          type="text"
                          className="form-control"
                          value={ProductName}
                          onChange={ this.changeProductName}
                        />
                      </div>
                      <div className="input-group mb-3">
                        <span className="input-group-text">Brand</span>
                        <input
                          type="text"
                          className="form-control"
                          value={Brand}
                          onChange={ this.changeBrand}
                        />
                      </div>
                      <div className="input-group mb-3">
                        <span className="input-group-text">Color</span>
                        <input
                          type="text"
                          className="form-control"
                          value={Color}
                          onChange={ this.changeColor}
                        />
                      </div>
                      <div className="input-group mb-3">
                        <span className="input-group-text">Size</span>
                        <input
                          type="text"
                          className="form-control"
                          value={Size}
                          onChange={ this.changeSize}
                        />
                      </div>
                      <div className="input-group mb-3">
                        <span className="input-group-text">Price</span>
                        <input
                          type="text"
                          className="form-control"
                          value={Price}
                          onChange={ this.changePrice}
                        />
                      </div>
                      <div className="input-group mb-3">
                        <span className="input-group-text">Quantity</span>
                        <input
                          type="text"
                          className="form-control"
                          value={Quantity}
                          onChange={ this.changeQuantity}
                        />
                      </div>
                    </div>
                    <div className="modal-footer">
                      <button
                        type="button"
                        className="btn btn-primary"
                        onClick={() => this.saveClick()}
                      >
                        Save
                      </button>
                      <button
                        type="button"
                        className="btn btn-secondary"
                        data-bs-dismiss="modal"
                      >
                        Close
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          );
                  }
                  }
                        