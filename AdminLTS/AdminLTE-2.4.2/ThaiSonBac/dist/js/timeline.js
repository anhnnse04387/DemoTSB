// DOM element where the Timeline will be attached
var timeline = document.getElementById('timeline');

var groups = new vis.DataSet([
  { id: 0, content: 'Order 1' },
  { id: 1, content: 'Order 2' },
  { id: 2, content: 'Order 3' },
  { id: 3, content: 'Order 4' },
]);

// create a dataset with items
var items = new vis.DataSet([
  {id: 0, group: 0, content: 'Tạo đơn', start: new Date(2018, 0, 01)},
  {id: 1, group: 0, content: 'Giao hàng', start: new Date(2018, 0, 03)},
  {id: 2, group: 1, content: 'Tạo đơn', start: new Date(2018, 0, 02)},
  {id: 3, group: 1, content: 'Giao hàng', start: new Date(2018, 0, 03)},
  {id: 4, group: 2, content: 'Tạo đơn', start: new Date(2018, 0, 01)},
  {id: 5, group: 2, content: 'Giao hàng', start: new Date(2018, 0, 04)},
  {id: 6, group: 3, content: 'Tạo đơn', start: new Date(2018, 0, 03)},
  {id: 7, group: 3, content: 'Giao hàng', start: new Date(2018, 0, 07)},
  {id: 8, group: 4, content: 'Tạo đơn', start: new Date(2018, 0, 03)},
  {id: 9, group: 4, content: 'Giao hàng', start: new Date(2018, 0, 04)}
]);

// Configuration for the Timeline
var options = {
  stack: true,
  horizontalScroll: true,
  maxHeight: 400,
  margin: {
    item: 10, // minimal margin between items
    axis: 5   // minimal margin between items and the axis
  }
};

// Create a Timeline
var timeline = new vis.Timeline(timeline, items, groups, options);
