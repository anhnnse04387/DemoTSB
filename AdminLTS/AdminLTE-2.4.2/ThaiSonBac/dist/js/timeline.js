// DOM element where the Timeline will be attached
var container = document.getElementById('timeline');

// Create a DataSet (allows two way data-binding)
var items = new vis.DataSet([
  {content: 'item 1 (100)', start: '2018-01-01'},
  {content: 'item 2 (200)', start: '2018-01-02'},
  {content: 'item 3 (200)', className: 'red', start: '2018-01-02T15:00:00'},
  {content: 'item 4 (200)', start: '2018-01-04'},
  {content: 'item 5 (300)', className: 'red', start: '2018-01-05'}
]);

// Configuration for the Timeline
var options = {
};

// Create a Timeline
var timeline = new vis.Timeline(container, items, options);
