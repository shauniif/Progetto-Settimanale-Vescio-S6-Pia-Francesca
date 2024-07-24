// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let pathCostumer = '/api/Customer'
let pathRoom = '/api/Room'
$(() => {
        $.ajax({
        url: `${pathCostumer}`,
            method: 'GET',
            success: (data) => {
                let customerSelect = $('#customer');
                customerSelect.empty();
                customerSelect.append('<option value="">Select a customer</option>');

                
                $(data).each((_, customer) => {
                    customerSelect.append(`<option value="${customer.id}">${customer.firstName} ${customer.lastName} </option>`);
                });
            }
        })

    $.ajax({
        url: `${pathRoom}`,
        method: 'GET',
        success: (data) => {
            let roomSelect = $('#room');
            roomSelect.empty();
            roomSelect.append('<option value=""> Select a room</option>');
            $(data).each((_, room) => {
                roomSelect.append(`<option value="${room.id}">${room.numberRoom}</option>`)
            })
        }
    })
}) 

