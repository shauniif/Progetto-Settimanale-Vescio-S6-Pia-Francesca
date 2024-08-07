﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let pathCostumer = '/api/CustomerApi'
let pathRoom = '/api/RoomApi'
let pathAdditionalService = '/api/AdditionalServiceApi'
let pathBooking = '/api/BookingApi'
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

    $.ajax({
        url: `${pathAdditionalService}`,
        method: 'GET',
        success: (data) => {
            let additionalServiceSelect = $('#additionalService')
            additionalServiceSelect.empty();
            additionalServiceSelect.append('<option value="">Select a additional service</option>');
            $(data).each((_, adds) => {
                additionalServiceSelect.append(`
                <option value="${adds.id}">${adds.typeOfService} </option>`);
            })
        }
    })

    $.ajax({
        url: `${pathBooking}`,
        method: 'GET',
        success: (data) => {
            let customerSelect = $('#booking');
            customerSelect.empty();
            customerSelect.append('<option value="">Select a booking</option>');


            $(data).each((_, booking) => {
                customerSelect.append(`<option value="${booking.id}">${booking.id}</option>`);
            });
        }
    })
    $("#research1").on('click', () => {
        let fiscalCode = $("#fiscalCode").val()
        $.ajax({
            url: `${pathBooking}/${fiscalCode}`,
            method: 'GET',
            success: (data) => {
                let bookingList = $("#booking-list")
                bookingList.empty()
                $(data).each((_, booking) => {
                    bookingList.append(`
                    <div class="card col">
                        <div class="card-body">
                    <p class="card-text" ><strong> Numero prenotazione:</strong> ${booking.id}</p>
                    <p class="card-text" ><strong> Data della prenotazione :</strong> ${booking.dateBooking}</p>
                    <p class="card-text" ><strong> Data di inizio soggiorno:</strong> ${booking.dateStart}</p>
                    <p class="card-text" ><strong> Data di fine soggiorno: </strong> ${booking.dateEnd}</p>
                    <p class="card-text" ><strong> Caparra: </strong> ${booking.deposit}€</p>
                    <p class="card-text" ><strong> Tariffa applicata:</strong>  ${booking.rate}€</p>
                        </div>
                    <div>
                    `)
                })
            }
        })
    })
    
    $("#research2").on('click', () => {
        let countBooking = $("#typeofStay").val()
        $.ajax({
            url: `${pathBooking}/Count/${countBooking}`,
            method: 'GET',
            success: (data) => {
                console.log(data)
                let typeResearched = $("#typeresearched")
                let numberOfType = $("#numberofType")
                typeResearched.text(countBooking);
                numberOfType.text(data);  
                }
            })
        })
    })

