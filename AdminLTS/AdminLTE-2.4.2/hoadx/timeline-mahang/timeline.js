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
  {id: 0, group: 0, content: '100', start: new Date(2018, 0, 03), end: new Date(2018, 0, 04)},
  {id: 1, group: 1, content: '200', start: new Date(2018, 0, 04), end: new Date(2018, 0, 05)},
  {id: 2, group: 2, content: '300', start: new Date(2018, 0, 05), end: new Date(2018, 0, 06)},
  {id: 3, group: 3, content: '500', start: new Date(2018, 0, 07), end: new Date(2018, 0, 08)}
]);

// Configuration for the Timeline
var options = {
  stack: true,
  horizontalScroll: true,
  maxHeight: 400,
  start: new Date(2018, 0, 01),
  end: new Date(2018, 0, 08),
  margin: {
    item: 10, // minimal margin between items
    axis: 5   // minimal margin between items and the axis
  }
};

// Create a Timeline
var timeline = new vis.Timeline(timeline, items, groups, options);
